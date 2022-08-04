using System;
using FlaxEditor;
using FlaxEditor.CustomEditors;
using FlaxEditor.GUI.Docking;
using FlaxEditor.SceneGraph;
using FlaxEditor.Windows;
using FlaxEngine;
using FlaxEngine.GUI;

namespace MultiTool
{
	public class MeshModifier : EditorWindow
	{
		private Label _label;
		private SceneGraphNode _selection;

		public MeshModifier(Editor editor) : base(editor, true, ScrollBars.None)
		{
			Title = "Mesh Modifier";
			ScrollMargin = new Margin(0, 0, 0, 100.0f);

			_label = new Label()
			{
				Parent = this,
				Text = GetSelectedItem()
			};
			editor.SceneEditing.SelectionChanged += SelectionChange;
		}

		private void SelectionChange()
		{
			_label.Text = GetSelectedItem();
		}

		private string GetSelectedItem()
		{
			if (Editor.SceneEditing.Selection.Count != 1)
				return "MORE THAN ONE OR NOTHING!";
			_selection = Editor.SceneEditing.Selection[0];
			return _selection.EditableObject.GetType().Name;
		}

		private void OnDebugDraw(GPUContext arg0, ref RenderContext arg1)
		{
			if (_selection.EditableObject is Actor actor)
			{
				DebugDraw.DrawWireBox(actor.BoxWithChildren, Color.Blue, 0f, false);
			}
		}

		public void ShowAutoAlign()
		{
			var sceneWin = Editor.Windows.SceneWin;
			// ADD BOX RENDER. CURRENT NOT WORKING
			MainRenderTask.Instance.PostRender += OnDebugDraw;
			if (sceneWin.IsDocked)
			{
				var bottom = sceneWin.Height > sceneWin.Width;
				Show(bottom ? DockState.DockBottom : DockState.DockRight, sceneWin);
				return;
			}
			Show();
		}

		protected override void OnClose()
		{
			MainRenderTask.Instance.PostRender -= OnDebugDraw;
		}
	}
}