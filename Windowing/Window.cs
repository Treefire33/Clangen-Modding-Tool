using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using System.ComponentModel;

namespace ClanGenModTool.Windowing;

public class Window : GameWindow
{
	private readonly ImGuiController _controller;

	public Window() : 
		base(GameWindowSettings.Default, 
			new NativeWindowSettings {ClientSize = new Vector2i(1600, 900), 
				APIVersion = new Version(4, 1), 
				MaximumClientSize = new Vector2i(1980, 1080), 
				MinimumClientSize = new Vector2i(1600, 90)
			})
	{ _controller = new ImGuiController(1600, 900); }

	public string WindowTitle;
	public Action LoadEvent, DrawEvent, CloseEvent;

	protected override void OnLoad()
	{
		base.OnLoad();
		VSync = VSyncMode.Adaptive;
		LoadEvent.Invoke();
		//Title = WindowTitle;
	}

	protected override void OnResize(ResizeEventArgs e)
	{
		base.OnResize(e);

		// Update the opengl viewport
		GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

		// Tell ImGui of the new size
		try
		{
			Console.WriteLine("ClientSize attempt");
			_controller.WindowResized(ClientSize.X, ClientSize.Y);
		} 
		catch
		{
			try
			{
				Console.WriteLine("Size attempt");
				_controller.WindowResized(Size.X, Size.Y);
			}
			catch
			{
				Console.WriteLine("Failure on all accounts.\ntry not to resize the window.");
			}
		}
	}

	protected override void OnRenderFrame(FrameEventArgs e)
	{
		base.OnRenderFrame(e);

		_controller.Update(this, (float)e.Time);

		GL.ClearColor(new Color4(0, 32, 48, 255));
		GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

		// Enable Docking
		ImGui.DockSpaceOverViewport();
		Theme.SetImGuiTheme(ImGui.GetStyle());

		ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;

		DrawEvent.Invoke();

		_controller.Render();

		ImGuiController.CheckGLError("End of frame");

		SwapBuffers();
		Thread.Sleep(10);
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		CloseEvent.Invoke();
	}

	protected override void OnTextInput(TextInputEventArgs e)
	{
		base.OnTextInput(e);

		_controller.PressChar((char)e.Unicode);
	}

	protected override void OnMouseWheel(MouseWheelEventArgs e)
	{
		base.OnMouseWheel(e);

		_controller.MouseScroll(e.Offset);
	}
}