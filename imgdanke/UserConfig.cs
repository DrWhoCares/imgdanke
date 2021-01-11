using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace imgdanke
{
	internal class UserConfig
	{
		internal const string CONFIG_FILENAME = "imgdanke_UserConfig.json";

		#region Functions

		internal static UserConfig LoadConfig()
		{
			string pathToConfig = CONFIG_FILENAME;

			// See if it's where it's being called from, which in most cases, will be next to the executable
			if ( !FileOps.DoesFileExist(pathToConfig) )
			{
				pathToConfig = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
				pathToConfig ??= "";
				pathToConfig = Path.Combine(pathToConfig, CONFIG_FILENAME);

				// See if it's next to the executable via reflection
				if ( !FileOps.DoesFileExist(pathToConfig) )
				{
					return new UserConfig();
				}
			}

			var resultConfig = JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText(pathToConfig));
			resultConfig.Defaults();

			return resultConfig;
		}

		internal UserConfig()
		{
			Defaults();
			_shouldCheckForUpdatesOnStartup = true;
		}

		private void Defaults()
		{
			_imagemagickPathToExe ??= "";
			_pingoPathToExe ??= "";
			_sourceFolderPath ??= "";
			_outputFolderPath ??= "";
			_outputExtension ??= ".png";
			_magickCommandString ??= "";
			_pingoCommandString ??= "";
			_magickDither ??= "";
			_magickColorspace ??= "";
			_pingoOptimizeLevel ??= "";
			_newOutputFolderBaseName ??= "";
		}

		internal void SaveConfig()
		{
			File.WriteAllText(CONFIG_FILENAME, JsonConvert.SerializeObject(this));
		}
		#endregion

		#region Member Variables

		#region Internal

		private Point _lastWindowLocation;

		internal Point LastWindowLocation
		{
			get => _lastWindowLocation;
			set
			{
				_lastWindowLocation = value;
				SaveConfig();
			}
		}

		private bool _shouldStartMaximized;

		internal bool ShouldStartMaximized
		{
			get => _shouldStartMaximized;
			set
			{
				_shouldStartMaximized = value;
				SaveConfig();
			}
		}

		private bool _shouldCheckForUpdatesOnStartup;

		internal bool ShouldCheckForUpdatesOnStartup
		{
			get => _shouldCheckForUpdatesOnStartup;
			set
			{
				_shouldCheckForUpdatesOnStartup = value;
				SaveConfig();
			}
		}

		private bool _shouldDisableFailedToCheckForUpdatesMessage;

		internal bool ShouldDisableFailedToCheckForUpdatesMessage
		{
			get => _shouldDisableFailedToCheckForUpdatesMessage;
			set
			{
				_shouldDisableFailedToCheckForUpdatesMessage = value;
				SaveConfig();
			}
		}

		private bool _shouldSuppressPingoNotFoundWarning;

		internal bool ShouldSuppressPingoNotFoundWarning
		{
			get => _shouldSuppressPingoNotFoundWarning;
			set
			{
				_shouldSuppressPingoNotFoundWarning = value;
				SaveConfig();
			}
		}

		private Size _lastWindowSize;

		internal Size LastWindowSize
		{
			get => _lastWindowSize;
			set
			{
				_lastWindowSize = value;
				SaveConfig();
			}
		}

		private string _imagemagickPathToExe;

		internal string ImagemagickPathToExe
		{
			get => _imagemagickPathToExe;
			set
			{
				_imagemagickPathToExe = value;
				SaveConfig();
			}
		}

		private string _pingoPathToExe;

		internal string PingoPathToExe
		{
			get => _pingoPathToExe;
			set
			{
				_pingoPathToExe = value;
				SaveConfig();
			}
		}

		private string _sourceFolderPath;

		internal string SourceFolderPath
		{
			get => _sourceFolderPath;
			set
			{
				_sourceFolderPath = value;
				SaveConfig();
			}
		}

		private string _outputFolderPath;

		internal string OutputFolderPath
		{
			get => _outputFolderPath;
			set
			{
				_outputFolderPath = value;
				SaveConfig();
			}
		}

		private bool _shouldDeleteOriginals;

		internal bool ShouldDeleteOriginals
		{
			get => _shouldDeleteOriginals;
			set
			{
				_shouldDeleteOriginals = value;
				SaveConfig();
			}
		}

		private bool _shouldReplaceOriginals;

		internal bool ShouldReplaceOriginals
		{
			get => _shouldReplaceOriginals;
			set
			{
				_shouldReplaceOriginals = value;
				SaveConfig();
			}
		}

		private string _outputExtension;

		internal string OutputExtension
		{
			get => _outputExtension;
			set
			{
				_outputExtension = value;
				SaveConfig();
			}
		}

		private bool _shouldIncludeSubfolders;

		internal bool ShouldIncludeSubfolders
		{
			get => _shouldIncludeSubfolders;
			set
			{
				_shouldIncludeSubfolders = value;
				SaveConfig();
			}
		}

		private bool _shouldMaintainFolderStructure;

		internal bool ShouldMaintainFolderStructure
		{
			get => _shouldMaintainFolderStructure;
			set
			{
				_shouldMaintainFolderStructure = value;
				SaveConfig();
			}
		}

		private bool _shouldIncludePSDs;

		internal bool ShouldIncludePSDs
		{
			get => _shouldIncludePSDs;
			set
			{
				_shouldIncludePSDs = value;
				SaveConfig();
			}
		}

		private string _magickCommandString;

		internal string MagickCommandString
		{
			get => _magickCommandString;
			set
			{
				_magickCommandString = value;
				SaveConfig();
			}
		}

		private string _pingoCommandString;

		internal string PingoCommandString
		{
			get => _pingoCommandString;
			set
			{
				_pingoCommandString = value;
				SaveConfig();
			}
		}

		private PresetSettings _presetSetting;

		internal PresetSettings PresetSetting
		{
			get => _presetSetting;
			set
			{
				_presetSetting = value;
				SaveConfig();
			}
		}

		#endregion

		#region Imagemagick Settings

		private bool _shouldAvoidMagickPNGCompression;

		internal bool ShouldAvoidMagickPNGCompression
		{
			get => _shouldAvoidMagickPNGCompression;
			set
			{
				_shouldAvoidMagickPNGCompression = value;
				SaveConfig();
			}
		}

		private string _magickDither;

		internal string MagickDither
		{
			get => _magickDither;
			set
			{
				_magickDither = value;
				SaveConfig();
			}
		}

		private string _magickColorspace;

		internal string MagickColorspace
		{
			get => _magickColorspace;
			set
			{
				_magickColorspace = value;
				SaveConfig();
			}
		}

		private int _magickColorsValue;

		internal int MagickColorsValue
		{
			get => _magickColorsValue;
			set
			{
				_magickColorsValue = value;
				SaveConfig();
			}
		}

		private int _magickDepthValue;

		internal int MagickDepthValue
		{
			get => _magickDepthValue;
			set
			{
				_magickDepthValue = value;
				SaveConfig();
			}
		}

		private int _magickPosterizeValue;

		internal int MagickPosterizeValue
		{
			get => _magickPosterizeValue;
			set
			{
				_magickPosterizeValue = value;
				SaveConfig();
			}
		}

		private bool _shouldUseMagickNormalize;

		internal bool ShouldUseMagickNormalize
		{
			get => _shouldUseMagickNormalize;
			set
			{
				_shouldUseMagickNormalize = value;
				SaveConfig();
			}
		}

		private bool _shouldUseMagickContrastStretch;

		internal bool ShouldUseMagickContrastStretch
		{
			get => _shouldUseMagickContrastStretch;
			set
			{
				_shouldUseMagickContrastStretch = value;
				SaveConfig();
			}
		}

		private bool _shouldUseMagickAutoLevel;

		internal bool ShouldUseMagickAutoLevel
		{
			get => _shouldUseMagickAutoLevel;
			set
			{
				_shouldUseMagickAutoLevel = value;
				SaveConfig();
			}
		}

		#endregion

		#region pingo Settings

		private int _pingoPNGPaletteValue;

		internal int PingoPNGPaletteValue
		{
			get => _pingoPNGPaletteValue;
			set
			{
				_pingoPNGPaletteValue = value;
				SaveConfig();
			}
		}

		private bool _shouldUsePingoNoDithering;

		internal bool ShouldUsePingoNoDithering
		{
			get => _shouldUsePingoNoDithering;
			set
			{
				_shouldUsePingoNoDithering = value;
				SaveConfig();
			}
		}

		private string _pingoOptimizeLevel;

		internal string PingoOptimizeLevel
		{
			get => _pingoOptimizeLevel;
			set
			{
				_pingoOptimizeLevel = value;
				SaveConfig();
			}
		}

		private bool _shouldUsePingoStrip;

		internal bool ShouldUsePingoStrip
		{
			get => _shouldUsePingoStrip;
			set
			{
				_shouldUsePingoStrip = value;
				SaveConfig();
			}
		}

		#endregion

		#region OutputSettings

		private bool _shouldOutputToNewFolder;

		internal bool ShouldOutputToNewFolder
		{
			get => _shouldOutputToNewFolder;
			set
			{
				_shouldOutputToNewFolder = value;
				SaveConfig();
			}
		}

		private bool _shouldUseSourceFolderAsOutputFolder;

		internal bool ShouldUseSourceFolderAsOutputFolder
		{
			get => _shouldUseSourceFolderAsOutputFolder;
			set
			{
				_shouldUseSourceFolderAsOutputFolder = value;
				SaveConfig();
			}
		}

		private string _newOutputFolderBaseName;

		internal string NewOutputFolderBaseName
		{
			get => _newOutputFolderBaseName;
			set
			{
				_newOutputFolderBaseName = value;
				SaveConfig();
			}
		}

		private string _newOutputFolderPath;

		internal string NewOutputFolderPath
		{
			get => _newOutputFolderPath;
			set
			{
				_newOutputFolderPath = value;
				SaveConfig();
			}
		}

		private string _tagsStringToAppendToFilenames;

		internal string TagsStringToAppendToFilenames
		{
			get => _tagsStringToAppendToFilenames;
			set
			{
				_tagsStringToAppendToFilenames = value;
				SaveConfig();
			}
		}

		#region TagsToFilenames

		private bool _shouldAddTagsToFilenames;

		internal bool ShouldAddTagsToFilenames
		{
			get => _shouldAddTagsToFilenames;
			set
			{
				_shouldAddTagsToFilenames = value;
				SaveConfig();
			}
		}

		private bool _shouldAddPresetToFilenames;

		internal bool ShouldAddPresetToFilenames
		{
			get => _shouldAddPresetToFilenames;
			set
			{
				_shouldAddPresetToFilenames = value;
				SaveConfig();
			}
		}

		private bool _shouldAddMagickSettingsToFilenames;

		internal bool ShouldAddMagickSettingsToFilenames
		{
			get => _shouldAddMagickSettingsToFilenames;
			set
			{
				_shouldAddMagickSettingsToFilenames = value;
				SaveConfig();
			}
		}

		private bool _shouldAddPingoSettingsToFilenames;

		internal bool ShouldAddPingoSettingsToFilenames
		{
			get => _shouldAddPingoSettingsToFilenames;
			set
			{
				_shouldAddPingoSettingsToFilenames = value;
				SaveConfig();
			}
		}

		private bool _shouldAddTagsToOutputFolder;

		internal bool ShouldAddTagsToOutputFolder
		{
			get => _shouldAddTagsToOutputFolder;
			set
			{
				_shouldAddTagsToOutputFolder = value;
				SaveConfig();
			}
		}

		private bool _shouldAddPresetToOutputFolder;

		internal bool ShouldAddPresetToOutputFolder
		{
			get => _shouldAddPresetToOutputFolder;
			set
			{
				_shouldAddPresetToOutputFolder = value;
				SaveConfig();
			}
		}

		private bool _shouldAddMagickSettingsToOutputFolder;

		internal bool ShouldAddMagickSettingsToOutputFolder
		{
			get => _shouldAddMagickSettingsToOutputFolder;
			set
			{
				_shouldAddMagickSettingsToOutputFolder = value;
				SaveConfig();
			}
		}

		private bool _shouldAddPingoSettingsToOutputFolder;

		internal bool ShouldAddPingoSettingsToOutputFolder
		{
			get => _shouldAddPingoSettingsToOutputFolder;
			set
			{
				_shouldAddPingoSettingsToOutputFolder = value;
				SaveConfig();
			}
		}

		#endregion

		#endregion

		#endregion
	}

	internal enum PresetSettings
	{
		Invalid = 0,
		None,
		Custom,
		Gray1Bpp,
		Gray4Bpp,
		Gray8Bpp,
		PingoColor4Bpp,
		PingoColor8Bpp,
		MagickColor4Bpp,
		MagickColor8Bpp
	}

	internal enum MagickDitherOptions
	{
		Invalid = 0,
		None
	}

	internal enum MagickColorspaceOptions
	{
		Invalid = 0,
		Gray,
		sRGB
	}

	internal enum PingoOptimizationLevels
	{
		Off = 0,
		s0,
		s1,
		s2,
		s3,
		s4,
		s5,
		s6,
		s7,
		s8,
		s9,
		sa,
		sb,
		Min = s0,
		Max = sb,
		Best = s9
	}
}
