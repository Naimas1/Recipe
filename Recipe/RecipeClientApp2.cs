
using Ical.Net;
using System.Windows;

namespace RecipeClientApp
{
    public partial class RecipeCard : UserControl
    {
        public RecipeCard()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public string RecipeName
        {
            get { return LblRecipeName.Text; }
            set { LblRecipeName.Text = value; }
        }

        public string Ingredients
        {
            get { return lblIngredients.Text; }
            set { lblIngredients.Text = value; }
        }

        public string Instructions
        {
            get { return txtInstructions.Text; }
            set { txtInstructions.Text = value; }
        }

        public static System.Drawing.Image GetRecipeImage1()
        { return pictureBox.Image; }

        public static void SetRecipeImage1(System.Drawing.Image value)
        { pictureBox.Image = value; }
    }

    internal class pictureBox
    {
        internal static System.Drawing.Image Image;
    }

    internal class txtInstructions
    {
        internal static string Text;
    }

    internal class lblIngredients
    {
        internal static string Text;
    }

    internal class lblRecipeName
    {
        internal static string Text;
    }

    public class UserControl
    {
        private Forms.Label lblRecipeName;
        private Forms.Label lblIngredients;
        private Forms.TextBox txtInstructions;
        private Forms.PictureBox pictureBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (Components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblRecipeName = new System.Windows.Forms.Label();
            this.lblIngredients = new System.Windows.Forms.Label();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecipeName
            // 
            this.lblRecipeName.AutoSize = true;
            this.lblRecipeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecipeName.Location = new System.Drawing.Point(3, 0);
            this.lblRecipeName.Name = "lblRecipeName";
            this.lblRecipeName.Size = new System.Drawing.Size(123, 25);
            this.lblRecipeName.TabIndex = 0;
            this.lblRecipeName.Text = "Recipe Name";
            // 
            // lblIngredients
            // 
            this.lblIngredients.AutoSize = true;
            this.lblIngredients.Location = new System.Drawing.Point(4, 34);
            this.lblIngredients.Name = "lblIngredients";
            this.lblIngredients.Size = new System.Drawing.Size(76, 17);
            this.lblIngredients.TabIndex = 1;
            this.lblIngredients.Text = "Ingredients";
            // 
            // txtInstructions
            // 
            this.txtInstructions.Location = new System.Drawing.Point(7, 64);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.ReadOnly = true;
            this.txtInstructions.Size = new System.Drawing.Size(250, 150);
            this.txtInstructions.TabIndex = 2;
            this.txtInstructions.Text = "Instructions";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(263, 4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(150, 150);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // RecipeCard
            // 
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.txtInstructions);
            this.Controls.Add(this.lblIngredients);
            this.Controls.Add(this.lblRecipeName);
            this.Name = "RecipeCard";
            this.Size = new System.Drawing.Size(420, 220);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SuspendLayout()
        {
            throw new NotImplementedException();
        }
    }

    internal class components
    {
        internal static void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
