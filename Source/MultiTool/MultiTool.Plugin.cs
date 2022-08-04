using System;
using System.Collections.Generic;
using FlaxEditor;
using FlaxEditor.GUI.ContextMenu;
using FlaxEngine;
using FlaxEngine.GUI;

namespace MultiTool
{
    /// <summary>
	/// MultiTool plugin.
	/// </summary>
	internal class PluginInfo : EditorPlugin
    {
        private PluginInfo _previousInstance;
        private ContextMenuChildMenu _elementList;
		private bool _initialized = false;

#if FLAX_1_3 || FLAX_1_2 || FLAX_1_1 || FLAX_1_0
		/// <inheritdoc />
		public override PluginDescription Description => new PluginDescription()
        {
            Name = "MultiTool",
            Category = "Editor",
            Author = "Crawcik",
            AuthorUrl = "https://github.com/Crawcik",
            HomepageUrl = "https://github.com/Crawcik/MultiTool",
            RepositoryUrl = "https://github.com/Crawcik/MultiTool",
            Description = "Collection of tools and functions that you might just need for your project!",
            Version = new Version(1, 0),
            IsAlpha = true,
            IsBeta = false,
		};
#else
		public PluginInfo() : base()
		{
            _description = new PluginDescription()
            {
                Name = "MultiTool",
                Category = "Editor",
                Author = "Crawcik",
                AuthorUrl = "https://github.com/Crawcik",
                HomepageUrl = "https://github.com/Crawcik/MultiTool",
                RepositoryUrl = "https://github.com/Crawcik/MultiTool",
                Description = "Collection of tools and functions that you might just need for your project!",
                Version = new Version(1, 0),
                IsAlpha = true,
                IsBeta = false,
            };
        }
        #endif

        public override void InitializeEditor()
        {
            if (_previousInstance)
            {
                if (_previousInstance == this)
                {
                    _previousInstance = null;
                    Deinitialize();
                    return;
                }
                _previousInstance.Deinitialize();
            }
            _previousInstance = this;
            Editor.Windows.SceneWin.ContextMenuShow += ContextMenuShow;
        }

		public override void Deinitialize()
		{
            if (Editor.Windows != null)
                Editor.Windows.SceneWin.ContextMenuShow -= ContextMenuShow;
            _elementList?.DisposeChildren();
            _elementList?.Dispose();
		}

		private void ContextMenuShow(ContextMenu contextMenu)
		{
            _elementList = contextMenu.AddChildMenu("MultiTool");
			var menu = _elementList.ContextMenu;

            menu.AddButton("Mesh Modifier", () => new MeshModifier(Editor).ShowAutoAlign());
		}
	}
}
