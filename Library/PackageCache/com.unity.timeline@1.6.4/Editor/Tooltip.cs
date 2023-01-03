using UnityEngine;

namespace UnityEditor.Timeline
{
    class Tooltip
    {
        public GUIStyle style { get; set; }

        public string text { get; set; }

        GUIStyle m_Font;

        public GUIStyle font
        {
            get
            {
                if (m_Font != null)
                    return m_Font;

                if (style != null)
                    return style;

                // Default Font.
                m_Font = new GUIStyle();
                m_Font.font = EditorS