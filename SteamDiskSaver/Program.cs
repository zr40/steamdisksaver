using System;
using System.Windows.Forms;

namespace SteamDiskSaver
{
	internal static class Program
	{
		internal static readonly Context Context = new Context();

		[STAThread]
		private static void Main()
		{
			Context.Run();
		}
	}

	internal sealed class Context : ApplicationContext
	{
		private int forms;

		internal Context()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
		}

		internal void Run()
		{
			new LoadingForm().Show();
			Application.Run(this);
		}

		internal void FormOpened()
		{
			forms++;
		}

		internal void FormClosed()
		{
			forms--;

			if (forms == 0)
			{
				ExitThread();
			}
		}
	}
}