﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Foreman
{
	public partial class RecipeChooserControl : UserControl
	{
		public Recipe DisplayedRecipe;
		public String text = "";

		public RecipeChooserControl(Recipe recipe, String text)
		{
			InitializeComponent();
			DisplayedRecipe = recipe;
			this.text = text;
		}

		private void RecipeChooserOption_Load(object sender, EventArgs e)
		{
			nameLabel.Text = String.Format(text, DisplayedRecipe.FriendlyName);
			foreach (Item ingredient in DisplayedRecipe.Ingredients.Keys)
			{
				inputListBox.Items.Add(String.Format("{0} ({1})", ingredient.FriendlyName, DisplayedRecipe.Ingredients[ingredient]));
			}
			foreach (Item result in DisplayedRecipe.Results.Keys)
			{
				outputListBox.Items.Add(String.Format("{0} ({1})", result.FriendlyName, DisplayedRecipe.Results[result]));
			}
			iconPictureBox.Image = DisplayedRecipe.Icon;
			iconPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

			RegisterMouseEvents(this);
		}

		private void RegisterMouseEvents(Control control)
		{
			control.MouseMove += MouseMoved;
			control.MouseClick += MouseClicked;

			foreach (Control subControl in control.Controls)
			{
				RegisterMouseEvents(subControl);
			}
		}

		private void MouseMoved(object sender, MouseEventArgs e)
		{
			BackColor = Color.HotPink;
			(FindForm() as RecipeChooserForm).SelectedControl = this;
		}

		private void MouseClicked(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				(FindForm() as RecipeChooserForm).SelectedControl = this;
				(FindForm() as RecipeChooserForm).DialogResult = DialogResult.OK;
				(FindForm() as RecipeChooserForm).Close();
			}
		}
	}
}
