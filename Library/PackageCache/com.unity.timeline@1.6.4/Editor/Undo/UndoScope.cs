using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

namespace UnityEditor.Timeline
{
    /// <summary>
    /// Disposable scope object used to collect multiple items for Undo.
    ///     Automatically filters out duplicates
    /// </summary>
    struct UndoScope : IDisposable
    {
        private static readonly HashSet<UnityEngine.Object> s_ObjectsToUndo = new HashSet<Object>();
        private string m_Name;

        public 