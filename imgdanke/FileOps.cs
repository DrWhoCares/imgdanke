using System;
using System.IO;
using System.Windows.Forms;

namespace imgdanke
{

	internal static class FileOps
	{
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

		internal static bool Delete(string path)
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

		internal static bool Move(string origPath, string newPath, bool shouldDeleteIfExists = true)
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
				if ( shouldDeleteIfExists )
				{
					Delete(newPath);
				}
				else
				{
					DisplayMoveError("File to move's destination at 'newPath' (" + origPath + ") already exists and 'shouldDeleteIfExists' is false. This should never happen.");
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
	}

}