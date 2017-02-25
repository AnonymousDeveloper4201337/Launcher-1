﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Forms
{
    /// <summary>
    /// Class frmContentPacks.
    /// </summary>
    /// <seealso cref="Form" />
    public partial class frmContentPacks : Form
    {
        /// <summary>
        /// The file paths
        /// </summary>
        private List<string> _filePaths = new List<string>();

        /// <summary>
        /// The current directory
        /// </summary>
        private readonly string _currentDirectory = Directory.GetCurrentDirectory() + @"\";

        /// <summary>
        /// Initializes a new instance of the <see cref="frmContentPacks"/> class.
        /// </summary>
        public frmContentPacks()
        {
            InitializeComponent();
            string[] dirFiles = File.ReadAllLines(_currentDirectory + @"resources\contentpacks\pack-load-order.yaml");
            foreach (var item in dirFiles)
            {
                lstPacks.Items.Add(Path.GetFileName(item.Replace("- ", "")));
            }
        }

        /// <summary>
        /// Imports files
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Opening import dialog");
            OpenFileDialog importDialog = new OpenFileDialog()
            {
                Title = "Choose a Content Pack",
                Filter = "Multi files|*.mf",
            };
            DialogResult result = importDialog.ShowDialog();
            try
            {

                string originalFilePath = importDialog.FileName;
                string fileName = Path.GetFileName(originalFilePath);
                if (result == DialogResult.OK)
                {
                    if (!File.Exists(_currentDirectory + @"resources\contentpacks\" + fileName))
                    {
                        File.Copy(originalFilePath, _currentDirectory + @"resources\contentpacks\" + fileName, true);
                        lstPacks.Items.Add(fileName);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load that content pack.");
            }
            this.ActiveControl = null;
        }

        /// <summary>
        /// Applies the user settings.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            string contentPackFile = _currentDirectory + @"resources\contentpacks\pack-load-order.yaml";
            List<string> items = new List<string>();
            foreach (var item in lstPacks.Items)
            {
                items.Add(item.ToString());
            }
            try
            {
                if (File.Exists(contentPackFile))
                {
                    Console.WriteLine("File exists");
                    File.Delete(contentPackFile);
                }
                else
                {
                    Console.WriteLine("Pack loader doesn't exist");
                    File.Create(contentPackFile);
                    Console.WriteLine("Created pack loader");
                }
                using (StreamWriter writer = File.AppendText(contentPackFile))
                {
                    foreach (var item in items)
                    {
                        writer.WriteLine("- " + item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception with the packloader was thrown");
                Console.WriteLine("Type: {0}\n\tStacktrace: {1}", ex.GetType(), ex.StackTrace);
            }
            this.ActiveControl = null;
        }

        /// <summary>
        /// Removes the selected item from the list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("Selected index: " + lstPacks.SelectedIndex);
                int selectedIndex = lstPacks.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    File.Delete(_currentDirectory + @"resources\contentpacks\" + lstPacks.Items[selectedIndex]);
                    lstPacks.Items.RemoveAt(selectedIndex);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to remove the file from the contentpacks folder.");
            }
            this.ActiveControl = null;
        }

        /// <summary>
        /// Moves the item up.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMoveItemUp_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
            this.ActiveControl = null;
        }

        /// <summary>
        /// Moves the item down.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMoveItemDown_Click(object sender, EventArgs e)
        {
            MoveItem(1);
            this.ActiveControl = null;
        }

        /// <summary>
        /// Moves the item.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void MoveItem(int direction)
        {
            // Checking selected item
            if (lstPacks.SelectedItem == null || lstPacks.SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = lstPacks.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= lstPacks.Items.Count)
                return; // Index out of range - nothing to do

            object selected = lstPacks.SelectedItem;

            // Removing removable element
            lstPacks.Items.Remove(selected);
            // Insert it in new position
            lstPacks.Items.Insert(newIndex, selected);
            // Restore selection
            lstPacks.SetSelected(newIndex, true);
        }

        /// <summary>
        /// Closes the content packs form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
