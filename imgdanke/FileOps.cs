using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace imgdanke
{

	internal static class FileOps
	{
		private static readonly bool IS_UNIX = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

		internal static bool DoesFileExist(string path)
		{
			// File.Exists() can potentially throw, and if so, may cause issues, so we need to make it so that checking for a file always returns false if it fails
			try
			{
				return File.Exists(path);
			}
			catch ( Exception )
			{
				return false;
			}
		}

		internal static bool DoesDirectoryExist(string path)
		{
			// Directory.Exists() can potentially throw, and if so, may cause issues, so we need to make it so that checking for a directory always returns false if it fails
			try
			{
				return Directory.Exists(path);
			}
			catch ( Exception )
			{
				return false;
			}
		}

		internal static bool IsFileReady(string path)
		{
			// If the file can be opened for exclusive access it means that the file is no longer locked by another process
			try
			{
				using FileStream inputStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);
				return inputStream.Length > 0;
			}
			catch ( Exception )
			{
				return false;
			}
		}

		internal static bool DeleteFile(string path)
		{
			try
			{
				File.Delete(path);
				return true;
			}
			catch ( Exception e )
			{
				MessageBox.Show("Attempting to delete file at 'path' (" + path + ") threw an exception:\n\n" + e.Message,
					"Deleting file failed",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return false;
			}
		}

		internal static bool DeleteFolder(string path, bool shouldWaitUntilEmptyToDelete)
		{
			if ( !shouldWaitUntilEmptyToDelete && !IsFolderEmpty(path) )
			{
				return false; // Avoid deleting folders with any items in them
			}

			foreach ( string subfolderPath in Directory.EnumerateDirectories(path) )
			{
				DeleteFolder(subfolderPath, true);
			}

			try
			{
				if ( shouldWaitUntilEmptyToDelete )
				{
					DirectoryInfo folder = new(path);

					while ( DoesDirectoryExist(folder.FullName) )
					{
						if ( IsFolderEmpty(folder.FullName) )
						{
							folder.Delete();
							folder.Refresh();
						}
						else
						{
							folder.Refresh();
						}
					}
				}
				else
				{
					Directory.Delete(path);
				}

				return true;
			}
			catch ( Exception e )
			{
				MessageBox.Show("Attempting to delete folder at 'path' (" + path + ") threw an exception:\n\n" + e.Message,
					"Deleting folder failed",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return false;
			}
		}

		// Returns true if the folder is empty by the end of the operation. Returns false otherwise
		internal static bool DeleteFilesInFolder(string path)
		{
			if ( !DoesDirectoryExist(path) )
			{
				Directory.CreateDirectory(path);
				return true;
			}

			if ( IsFolderEmpty(path) )
			{
				return true;
			}

			if ( MessageBox.Show("The temp folder at 'path' (" + path + ") is not empty. This should only happen if you canceled the previous operation or something went wrong." + "\n\nClick OK to delete and continue, click Cancel to cancel processing.",
				"Temp folder is not empty",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Warning) == DialogResult.Cancel )
			{
				return false;
			}

			try
			{
				// Need to ensure the folder was deleted before attempting to recreate the directory, because Directory.Delete() will delete the root folder as well
				DeleteFolderRecursively(path);

				if ( !Directory.Exists(path) )
				{
					Directory.CreateDirectory(path);
				}

				return true;
			}
			catch ( Exception e )
			{
				MessageBox.Show("Attempting to delete folder at 'path' (" + path + ") threw an exception:\n\n" + e.Message,
					"Deleting folder failed",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return false;
			}
		}

		private static bool DeleteFolderRecursively(string path)
		{
			try
			{
				DirectoryInfo folder = new(path);
				folder.Delete(true);
				folder.Refresh();

				while ( DoesDirectoryExist(folder.FullName) )
				{
					folder.Refresh();
				}

				return true;
			}
			catch ( Exception e )
			{
				MessageBox.Show("Attempting to delete folder at 'path' (" + path + ") threw an exception:\n\n" + e.Message,
					"Deleting folder failed",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return false;
			}
		}

		private static bool IsFolderEmpty(string path)
		{
			try
			{
				return !Directory.EnumerateFileSystemEntries(path).Any();
			}
			catch ( DirectoryNotFoundException )
			{
				return true; // If we can't find the directory, assume it's been deleted
			}
			catch ( Exception e )
			{
				MessageBox.Show("Attempting to check if folder at 'path' (" + path + ") is empty threw an exception:\n\n" + e.Message,
					"Checking if folder is empty failed",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return false;
			}
		}

		internal static bool RenameFile(FileInfo file, string newName, bool shouldOverwrite = true)
		{
			return Move(file.FullName, file.DirectoryName + "/" + newName, shouldOverwrite);
		}

		internal static bool Move(string origPath, string newPath, bool shouldOverwrite = true)
		{
			if ( origPath == newPath )
			{
				return true; // No need to move, there is no change
			}

			if ( !DoesFileExist(origPath) )
			{
				DisplayMoveError("File to move at 'origPath' (" + origPath + ") does not exist. This should never happen.");
				return false;
			}

			if ( DoesFileExist(newPath) )
			{
				if ( shouldOverwrite )
				{
					DeleteFile(newPath);
				}
				else
				{
					DisplayMoveError("File to move's destination at 'newPath' (" + origPath + ") already exists and 'shouldOverwrite' is false. This should never happen.");
					return false;
				}
			}

			try
			{
				File.Move(origPath, newPath);
				return true;
			}
			catch ( Exception e )
			{
				DisplayMoveError("Attempting to move file at 'origPath' (" + origPath + ") to destination at 'newPath' (" + newPath + ") threw an exception:\n\n" + e.Message);
				return false;
			}
		}

		private static void DisplayMoveError(string errorMsg)
		{
			MessageBox.Show(errorMsg,
				"Moving file failed",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}

		internal static void OpenFolderPathInExplorer(string path)
		{
			if ( string.IsNullOrEmpty(path) || !DoesDirectoryExist(path) )
			{
				return;
			}

			if ( path.Last() != '/' && path.Last() != '\\' )
			{
				path += Path.DirectorySeparatorChar;
			}

			try
			{
				using Process process = Process.Start(path);
			}
			catch ( Exception ex )
			{
				MessageBox.Show("Unable to open the folder at `" + path + "`. Exception thrown:\n\n`" + ex.Message + "`",
					"Cannot open path to folder",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
		}

		internal static void OpenPathToFileInExplorer(string path, string filename)
		{
			if ( !DoesDirectoryExist(path) )
			{
				return;
			}

			string localPath = Path.Combine(path, filename);

			if ( !DoesFileExist(localPath) )
			{
				return;
			}

			try
			{
				if ( IS_UNIX )
				{
					using Process process = Process.Start("xdg-open", localPath);
				}
				else
				{
					using Process process = Process.Start("explorer.exe", "/select, \"" + localPath + "\"");
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show("Unable to open the path to `" + filename + "` at `" + localPath + "`. Exception thrown:\n\n`" + ex.Message + "`",
					"Cannot open path to " + filename,
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
		}

		internal static void OpenFileInDefaultApplication(string filepath)
		{
			if ( string.IsNullOrEmpty(filepath) || !DoesFileExist(filepath) )
			{
				return;
			}

			try
			{
				using Process process = Process.Start(filepath);
			}
			catch ( Exception ex )
			{
				MessageBox.Show("Unable to open the file at `" + filepath + "` in its default application. Exception thrown:\n\n`" + ex.Message + "`",
					"Cannot open file in default application",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
		}

		internal static void OpenFileInDefaultApplication(string path, string filename)
		{
			if ( string.IsNullOrEmpty(path) || string.IsNullOrEmpty(filename) || !DoesDirectoryExist(path) )
			{
				return;
			}

			OpenFileInDefaultApplication(Path.Combine(path, filename));
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

		private static bool CreateHardLinkToFile(FileInfo file, string pathToTempWorkingFolder)
		{
			try
			{
				if ( !IS_UNIX )
				{
					return CreateHardLink(pathToTempWorkingFolder + file.Name, file.FullName, IntPtr.Zero);
				}

				ProcessStartInfo startInfo = new()
				{
					FileName = "ln",
					CreateNoWindow = true,
					Arguments = "-s \"" + file.FullName + "\" \"" + pathToTempWorkingFolder + file.Name + "\""
				};

				using Process process = Process.Start(startInfo);

				if ( process == null )
				{
					return false;
				}

				process.WaitForExit(5000);
				return process.ExitCode == 0;

			}
			catch ( Exception ex )
			{
				MessageBox.Show("Unable to create a hard link to the file `" + file.FullName + "` at path `" + pathToTempWorkingFolder + file.Name + "`. Exception thrown:\n\n`" + ex.Message + "`\n\nProcessing will cancel.",
					"Cannot create hard link to file",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
				return false;
			}
		}

		// Returns true if it successfully creates the links to files. Returns false if something goes wrong
		internal static bool CreateHardLinksToFiles(Dictionary<string, List<FileInfo>> files, string tempFolderPath, string sourceFolderPath)
		{
			foreach ( var pathAndInfos in files )
			{
				string tempSubFolderPath = tempFolderPath + pathAndInfos.Key.Replace(sourceFolderPath, "") + "/";
				Directory.CreateDirectory(tempSubFolderPath);

				foreach ( FileInfo file in pathAndInfos.Value )
				{
					if ( !CreateHardLinkToFile(file, tempSubFolderPath) )
					{
						MessageBox.Show("Unable to create a hard link to the file `" + file.FullName + "` at path `" + tempSubFolderPath + file.Name + "`\n\nProcessing will cancel.",
							"Cannot create hard link to file",
							MessageBoxButtons.OK,
							MessageBoxIcon.Exclamation);
						return false;
					}
				}
			}

			return !IsFolderEmpty(tempFolderPath);
		}

	}

}
