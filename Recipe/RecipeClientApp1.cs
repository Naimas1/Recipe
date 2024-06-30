using RecipeServerApp;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace RecipeClientApp
{
    public partial class ClientForm : Form
    {
        private UdpClient udpClient;
        private IPEndPoint serverEndpoint;

        public ClientForm()
        {
            InitializeComponent();
            udpClient = new UdpClient();
            serverEndpoint = new IPEndPoint(IPAddress.Loopback, 11000); // Assuming server runs on localhost
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var ingredients = txtIngredients.Text.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var ingredientsList = new List<string>(ingredients);

            var requestString = JsonSerializer.Serialize(ingredientsList);
            var requestBytes = Encoding.UTF8.GetBytes(requestString);
            udpClient.Send(requestBytes, requestBytes.Length, serverEndpoint);

            var responseBytes = udpClient.Receive(ref serverEndpoint);
            var responseString = Encoding.UTF8.GetString(responseBytes);

            var recipes = JsonSerializer.Deserialize<List<Recipe>>(responseString);
            DisplayRecipes(recipes);
        }

        private void DisplayRecipes(List<Recipe> recipes)
        {
            recipePanel.Controls.Clear();

            foreach (var recipe in recipes)
            {
                var recipeCard = new RecipeCard();
                recipeCard.SetRecipeName(recipe.Name);
                recipeCard.SetIngredients(string.Join(", ", recipe.Ingredients));
                recipeCard.SetInstructions(recipe.Instructions);

                if (!string.IsNullOrEmpty(recipe.ImageBase64))
                {
                    var imageBytes = Convert.FromBase64String(recipe.ImageBase64);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        recipeCard.SetRecipeImage(System.Drawing.Image.FromStream(ms));
                    }
                }

                recipePanel.Controls.Add(recipeCard);
            }
        }
    }

    internal class RecipeCard
    {
        public RecipeCard()
        {
        }

        private string recipeName;

        public string GetRecipeName()
        {
            return recipeName;
        }

        internal void SetRecipeName(string value)
        {
            recipeName = value;
        }

        private string ingredients;

        public string GetIngredients()
        {
            return ingredients;
        }

        internal void SetIngredients(string value)
        {
            ingredients = value;
        }

        private string instructions;

        public string GetInstructions()
        {
            return instructions;
        }

        internal void SetInstructions(string value)
        {
            instructions = value;
        }

        private object recipeImage;

        public object GetRecipeImage()
        {
            return recipeImage;
        }

        internal void SetRecipeImage(object value)
        {
            recipeImage = value;
        }
    }

    internal class recipePanel
    {
        public static object Controls { get; internal set; }
    }

    internal class txtIngredients
    {
        public static object Text { get; internal set; }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string Instructions { get; set; }
        public string ImageBase64 { get; set; } 
    }
}
