using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace imgdanke
{
	internal class UserConfig
	{
		public const string CONFIG_FILENAME = "imgdanke_UserConfig.json";

		#region Functions
		public static UserConfig LoadConfig()
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

			if ( resultConfig._validInputExtensions.Count == 0 )
			{
				resultConfig._validInputExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".psd", ".tif", ".gif", ".webp" };
			}

			if ( resultConfig._validOutputExtensions.Count == 0 )
			{
				resultConfig._validOutputExtensions = new List<string> { ".png", ".jpg" };
			}

			return resultConfig;
		}

		public UserConfig()
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
			_validInputExtensions ??= new List<string>();
			_validOutputExtensions ??= new List<string>();
			_outputExtension ??= ".png";
			_magickCommandString ??= "";
			_pingoCommandString ??= "";
			_magickDither ??= "";
			_magickColorspace ??= "";
			_pingoOptimizeLevel ??= "";
			_newOutputFolderBaseName ??= "_danke";
		}

		public void SaveConfig()
		{
			File.WriteAllText(CONFIG_FILENAME, JsonConvert.SerializeObject(this));
		}

		#endregion

		#region Member Variables

		#region Internal

		private Point _lastWindowLocation;

		public Point LastWindowLocation
		{
			get => _lastWindowLocation;
			set
			{
				_lastWindowLocation = value;
				SaveConfig();
			}
		}

		private bool _shouldStartMaximized;

		public bool ShouldStartMaximized
		{
			get => _shouldStartMaximized;
			set
			{
				_shouldStartMaximized = value;
				SaveConfig();
			}
		}

		private bool _shouldCheckForUpdatesOnStartup;

		public bool ShouldCheckForUpdatesOnStartup
		{
			get => _shouldCheckForUpdatesOnStartup;
			set
			{
				_shouldCheckForUpdatesOnStartup = value;
				SaveConfig();
			}
		}

		private bool _shouldDisableFailedToCheckForUpdatesMessage;

		public bool ShouldDisableFailedToCheckForUpdatesMessage
		{
			get => _shouldDisableFailedToCheckForUpdatesMessage;
			set
			{
				_shouldDisableFailedToCheckForUpdatesMessage = value;
				SaveConfig();
			}
		}

		private bool _shouldSuppressPingoNotFoundWarning;

		public bool ShouldSuppressPingoNotFoundWarning
		{
			get => _shouldSuppressPingoNotFoundWarning;
			set
			{
				_shouldSuppressPingoNotFoundWarning = value;
				SaveConfig();
			}
		}

		private Size _lastWindowSize;

		public Size LastWindowSize
		{
			get => _lastWindowSize;
			set
			{
				_lastWindowSize = value;
				SaveConfig();
			}
		}

		private string _imagemagickPathToExe;

		public string ImagemagickPathToExe
		{
			get => _imagemagickPathToExe;
			set
			{
				_imagemagickPathToExe = value;
				SaveConfig();
			}
		}

		private string _pingoPathToExe;

		public string PingoPathToExe
		{
			get => _pingoPathToExe;
			set
			{
				_pingoPathToExe = value;
				SaveConfig();
			}
		}

		private string _sourceFolderPath;

		public string SourceFolderPath
		{
			get => _sourceFolderPath;
			set
			{
				_sourceFolderPath = value;
				SaveConfig();
			}
		}

		private string _outputFolderPath;

		public string OutputFolderPath
		{
			get => _outputFolderPath;
			set
			{
				_outputFolderPath = value;
				SaveConfig();
			}
		}

		private bool _shouldDeleteOriginals;

		public bool ShouldDeleteOriginals
		{
			get => _shouldDeleteOriginals;
			set
			{
				_shouldDeleteOriginals = value;
				SaveConfig();
			}
		}

		private bool _shouldReplaceOriginals;

		public bool ShouldReplaceOriginals
		{
			get => _shouldReplaceOriginals;
			set
			{
				_shouldReplaceOriginals = value;
				SaveConfig();
			}
		}

		private List<string> _validInputExtensions;

		public List<string> ValidInputExtensions
		{
			get => _validInputExtensions;
			set
			{
				_validInputExtensions = value;
				SaveConfig();
			}
		}

		private List<string> _validOutputExtensions;

		public List<string> ValidOutputExtensions
		{
			get => _validOutputExtensions;
			set
			{
				_validOutputExtensions = value;
				SaveConfig();
			}
		}

		private string _outputExtension;

		public string OutputExtension
		{
			get => _outputExtension;
			set
			{
				_outputExtension = value;
				SaveConfig();
			}
		}

		private bool _shouldIncludeSubfolders;

		public bool ShouldIncludeSubfolders
		{
			get => _shouldIncludeSubfolders;
			set
			{
				_shouldIncludeSubfolders = value;
				SaveConfig();
			}
		}

		private bool _shouldMaintainFolderStructure;

		public bool ShouldMaintainFolderStructure
		{
			get => _shouldMaintainFolderStructure;
			set
			{
				_shouldMaintainFolderStructure = value;
				SaveConfig();
			}
		}

		private string _magickCommandString;

		public string MagickCommandString
		{
			get => _magickCommandString;
			set
			{
				_magickCommandString = value;
				SaveConfig();
			}
		}

		private string _pingoCommandString;

		public string PingoCommandString
		{
			get => _pingoCommandString;
			set
			{
				_pingoCommandString = value;
				SaveConfig();
			}
		}

		private PresetSettings _presetSetting;

		public PresetSettings PresetSetting
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

		public bool ShouldAvoidMagickPNGCompression
		{
			get => _shouldAvoidMagickPNGCompression;
			set
			{
				_shouldAvoidMagickPNGCompression = value;
				SaveConfig();
			}
		}

		private string _magickDither;

		public string MagickDither
		{
			get => _magickDither;
			set
			{
				_magickDither = value;
				SaveConfig();
			}
		}

		private string _magickColorspace;

		public string MagickColorspace
		{
			get => _magickColorspace;
			set
			{
				_magickColorspace = value;
				SaveConfig();
			}
		}

		private int _magickColorsValue;

		public int MagickColorsValue
		{
			get => _magickColorsValue;
			set
			{
				_magickColorsValue = value;
				SaveConfig();
			}
		}

		private int _magickDepthValue;

		public int MagickDepthValue
		{
			get => _magickDepthValue;
			set
			{
				_magickDepthValue = value;
				SaveConfig();
			}
		}

		private int _magickPosterizeValue;

		public int MagickPosterizeValue
		{
			get => _magickPosterizeValue;
			set
			{
				_magickPosterizeValue = value;
				SaveConfig();
			}
		}

		private bool _shouldUseMagickNormalize;

		public bool ShouldUseMagickNormalize
		{
			get => _shouldUseMagickNormalize;
			set
			{
				_shouldUseMagickNormalize = value;
				SaveConfig();
			}
		}

		private bool _shouldUseMagickContrastStretch;

		public bool ShouldUseMagickContrastStretch
		{
			get => _shouldUseMagickContrastStretch;
			set
			{
				_shouldUseMagickContrastStretch = value;
				SaveConfig();
			}
		}

		private bool _shouldUseMagickAutoLevel;

		public bool ShouldUseMagickAutoLevel
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

		public int PingoPNGPaletteValue
		{
			get => _pingoPNGPaletteValue;
			set
			{
				_pingoPNGPaletteValue = value;
				SaveConfig();
			}
		}

		private bool _shouldUsePingoNoDithering;

		public bool ShouldUsePingoNoDithering
		{
			get => _shouldUsePingoNoDithering;
			set
			{
				_shouldUsePingoNoDithering = value;
				SaveConfig();
			}
		}

		private string _pingoOptimizeLevel;

		public string PingoOptimizeLevel
		{
			get => _pingoOptimizeLevel;
			set
			{
				_pingoOptimizeLevel = value;
				SaveConfig();
			}
		}

		private bool _shouldUsePingoStrip;

		public bool ShouldUsePingoStrip
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

		public bool ShouldOutputToNewFolder
		{
			get => _shouldOutputToNewFolder;
			set
			{
				_shouldOutputToNewFolder = value;
				SaveConfig();
			}
		}

		private bool _shouldUseSourceFolderAsOutputFolder;

		public bool ShouldUseSourceFolderAsOutputFolder
		{
			get => _shouldUseSourceFolderAsOutputFolder;
			set
			{
				_shouldUseSourceFolderAsOutputFolder = value;
				SaveConfig();
			}
		}

		private string _newOutputFolderBaseName;

		public string NewOutputFolderBaseName
		{
			get => _newOutputFolderBaseName;
			set
			{
				_newOutputFolderBaseName = value;
				SaveConfig();
			}
		}

		private string _newOutputFolderPath;

		public string NewOutputFolderPath
		{
			get => _newOutputFolderPath;
			set
			{
				_newOutputFolderPath = value;
				SaveConfig();
			}
		}

		private string _tagsStringToAppendToFilenames;

		public string TagsStringToAppendToFilenames
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

		public bool ShouldAddTagsToFilenames
		{
			get => _shouldAddTagsToFilenames;
			set
			{
				_shouldAddTagsToFilenames = value;
				SaveConfig();
			}
		}

		private bool _shouldAddPresetToFilenames;

		public bool ShouldAddPresetToFilenames
		{
			get => _shouldAddPresetToFilenames;
			set
			{
				_shouldAddPresetToFilenames = value;
				SaveConfig();
			}
		}

		private bool _shouldAddMagickSettingsToFilenames;

		public bool ShouldAddMagickSettingsToFilenames
		{
			get => _shouldAddMagickSettingsToFilenames;
			set
			{
				_shouldAddMagickSettingsToFilenames = value;
				SaveConfig();
			}
		}

		private bool _shouldAddPingoSettingsToFilenames;

		public bool ShouldAddPingoSettingsToFilenames
		{
			get => _shouldAddPingoSettingsToFilenames;
			set
			{
				_shouldAddPingoSettingsToFilenames = value;
				SaveConfig();
			}
		}

		private bool _shouldAddTagsToOutputFolder;

		public bool ShouldAddTagsToOutputFolder
		{
			get => _shouldAddTagsToOutputFolder;
			set
			{
				_shouldAddTagsToOutputFolder = value;
				SaveConfig();
			}
		}

		private bool _shouldAddPresetToOutputFolder;

		public bool ShouldAddPresetToOutputFolder
		{
			get => _shouldAddPresetToOutputFolder;
			set
			{
				_shouldAddPresetToOutputFolder = value;
				SaveConfig();
			}
		}

		private bool _shouldAddMagickSettingsToOutputFolder;

		public bool ShouldAddMagickSettingsToOutputFolder
		{
			get => _shouldAddMagickSettingsToOutputFolder;
			set
			{
				_shouldAddMagickSettingsToOutputFolder = value;
				SaveConfig();
			}
		}

		private bool _shouldAddPingoSettingsToOutputFolder;

		public bool ShouldAddPingoSettingsToOutputFolder
		{
			get => _shouldAddPingoSettingsToOutputFolder;
			set
			{
				_shouldAddPingoSettingsToOutputFolder = value;
				SaveConfig();
			}
		}

		#endregion

		#region TagsToOutputFolder



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
