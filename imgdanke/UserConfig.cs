using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace imgdanke
{
	internal class UserConfig
	{
		private const string CONFIG_FILENAME = "imgdanke_UserConfig.json";

		#region Functions
		public static UserConfig LoadConfig()
		{
			if ( !File.Exists(CONFIG_FILENAME) )
			{
				return new UserConfig();
			}

			var resultConfig = JsonConvert.DeserializeObject<UserConfig>(File.ReadAllText(CONFIG_FILENAME));
			resultConfig.Defaults();

			return resultConfig;
		}

		public UserConfig()
		{
			Defaults();
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
		}

		private void SaveConfig()
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

		private bool _shouldIncludePSDs;

		public bool ShouldIncludePSDs
		{
			get => _shouldIncludePSDs;
			set
			{
				_shouldIncludePSDs = value;
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

		private PingoAdditionalChecks _pingoAdditionalChecks;

		public PingoAdditionalChecks PingoAdditionalChecks
		{
			get => _pingoAdditionalChecks;
			set
			{
				_pingoAdditionalChecks = value;
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

	internal enum PingoAdditionalChecks
	{
		None = 0,
		sb,
		sa
	}

	internal enum PingoOptimizationLevels
	{
		s0 = 0,
		s1,
		s2,
		s3,
		s4,
		s5,
		s6,
		s7,
		s8,
		s9,
		Min = s0,
		Max = s9
	}
}
