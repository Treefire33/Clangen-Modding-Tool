using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NativeFileDialogSharp;
using System.IO;

namespace ClangenModTool.UI
{
	public class FileDialog
	{
		public string SelectedPath { get; set; } = "";

		public bool ShowDialog(string title = "File Select", string filters = "")
		{
			DialogResult dialogResult = Dialog.FileOpen(filters);
			SelectedPath = dialogResult.Path;
			return dialogResult.IsOk;
		}
		public bool ShowFolderDialog(string title = "File Select")
		{
			DialogResult dialogResult = Dialog.FolderPicker();
			SelectedPath = dialogResult.Path;
			return dialogResult.IsOk;
		}
	}
}
