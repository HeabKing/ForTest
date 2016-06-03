using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesinx.Utility
{
	/// <summary>
	/// 改变图片质量类 .jpg(有压缩) .jpeg(有压缩) .png(没压缩) .bmp(没压缩)
	/// </summary>
	/// <remarks>何士雄 2016-06-02</remarks>
	/// <example>200个jgp图片, 200M 7秒钟</example>
	public static class VaryImgQuality
	{
		/// <summary>
		/// 对单个文件进行压缩
		/// </summary>
		/// <param name="path">源文件</param>
		/// <param name="desDir">目标路径</param>
		/// <returns></returns>
		public static Task VaryAsync(string path, string desDir)
		{
			return VaryAsync(new[] { path }, desDir, 50);
		}

		/// <summary>
		/// 对指定指定中的所有图片进行异步并行压缩 - 目标文件跟源文件在同一目录 - 按默认的压缩参数
		/// </summary>
		/// <returns></returns>
		public static Task VaryAsync(string path)
		{
			return VaryAsync(new List<string> { path });
		}

		/// <summary>
		/// 对指定集合中的所有图片进行异步并行压缩 - 目标文件跟源文件在同一目录 - 按默认的压缩参数
		/// </summary>
		/// <param name="paths">源路径与目标路径的集合</param>
		/// <returns></returns>
		public static Task VaryAsync(IEnumerable<string> paths)
		{
			return VaryAsync(paths, 50);
		}

		/// <summary>
		/// 对指定集合中的所有图片进行异步并行压缩 - 将转换后的文件全部移动到目标路径中
		/// </summary>
		/// <param name="paths">源路径</param>
		/// <param name="desDir">目标路径</param>
		/// <returns></returns>
		public static Task VaryAsync(IEnumerable<string> paths, string desDir)
		{
			return VaryAsync(paths, desDir, 50);
		}

		/// <summary>
		/// 对指定集合中的所有图片进行异步并行压缩 - 目标文件跟源文件在同一目录
		/// </summary>
		/// <param name="paths">源路径与目标路径的集合</param>
		/// <param name="value">压缩的参数值 EncoderParameters* pEncoderParameters = (EncoderParameters*) malloc(sizeof(EncoderParameters) + (n-1) * sizeof(EncoderParameter));</param>
		/// <returns></returns>
		public static Task VaryAsync(IEnumerable<string> paths, long value)
		{
			var pkv = paths.Select(p => new KeyValuePair<string, string>(p, GetDesValue(p, value)));
			return VaryAsync(pkv, value);
		}

		/// <summary>
		/// 对指定集合中的所有图片进行异步并行压缩 - 将转换后的文件全部移动到目标路径中
		/// </summary>
		/// <param name="paths">源路径</param>
		/// <param name="desDir">目标路径</param>
		/// <param name="value">压缩的参数值 EncoderParameters* pEncoderParameters = (EncoderParameters*) malloc(sizeof(EncoderParameters) + (n-1) * sizeof(EncoderParameter));</param>
		/// <returns></returns>
		public static Task VaryAsync(IEnumerable<string> paths, string desDir, long value)
		{
			var dir = Path.GetDirectoryName(desDir);
			if (dir == null)
			{
				throw new ArgumentException(desDir);
			}
			Directory.CreateDirectory(dir);
			var pkv = paths.Select(p => new KeyValuePair<string, string>(p, GetDesValue(dir + "\\" + Path.GetFileName(p), value)));
			return VaryAsync(pkv, value);
		}

		/// <summary>
		/// 为了防止在保存的时候已经有一个同名文件, 而这个同名文件也正在被压缩, 所以保存代码无法删除这个正在被压缩的文件导致的错误, 所以每次保存新文件的时候创建一个GUID在并行批量处理是比较合适的
		/// </summary>
		/// <param name="p"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private static string GetDesValue(string p, long value)
		{
			return Path.GetDirectoryName(p) + "\\" + Path.GetFileNameWithoutExtension(p) + "_" + Guid.NewGuid() + "_" + value +
			Path.GetExtension(p);
		}

		/// <summary>
		/// 对指定集合中的所有图片进行异步并行压缩
		/// </summary>
		/// <param name="paths">源路径与目标路径的集合</param>
		/// <param name="value">压缩的参数值 EncoderParameters* pEncoderParameters = (EncoderParameters*) malloc(sizeof(EncoderParameters) + (n-1) * sizeof(EncoderParameter));</param>
		/// <returns></returns>
		public static async Task VaryAsync(IEnumerable<KeyValuePair<string, string>> paths, long value)
		{
			await Task.Run(() => Parallel.ForEach(paths, p =>
			{
				// 创建一个EncoderParameters对象, 一个包含EncoderParameter对象的数组对象EncoderParameters对象, 
				// 在本例中, 数组只需要一个EncoderParameter对象 
				using (var myEncoderParameters = new EncoderParameters(1)
				{
					Param = { [0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, value) }
				})
				using (Bitmap bmp = new Bitmap(p.Key))
				{
					// 获取编码信息
					ImageFormat format;
					switch (Path.GetExtension(p.Key))
					{
						case ".jpeg":
						case ".jpg":
							format = ImageFormat.Jpeg;
							break;
						case ".png":
							format = ImageFormat.Png;
							break; // 在50的压缩下并没有压缩
						case ".bmp":
							format = ImageFormat.Bmp;
							break; // 在50的压缩下并没有压缩
						default:
							throw new Exception("不支持的文件类型");
					}
					ImageCodecInfo imgEncoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(imgCodecInfo => imgCodecInfo.FormatID == format.Guid);
					if (imgEncoder == null) { throw new Exception($"找不到{p.Key}编码信息"); }
					string desPath = p.Value;
					bmp.Save(desPath, imgEncoder, myEncoderParameters);
					foreach (var item in myEncoderParameters.Param)
					{
						item.Dispose();
					}
				}
			})).ConfigureAwait(false);
		}
	}
}
