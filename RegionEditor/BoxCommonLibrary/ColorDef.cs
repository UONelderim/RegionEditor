using System;
using System.Xml.Serialization;

namespace TheBox.Common
{
	[ Serializable ]
	public class ColorDef
	{
		private int m_Red;
		private int m_Green;
		private int m_Blue;
		private int m_Alpha = 255;

		[ XmlAttribute ]
		public int Red
		{
			get => m_Red;
			set => m_Red = value;
		}

		[ XmlAttribute ]
		public int Green
		{
			get => m_Green;
			set => m_Green = value;
		}

		[ XmlAttribute ]
		public int Blue
		{
			get => m_Blue;
			set => m_Blue = value;
		}

		[ XmlAttribute ]
		public int Alpha
		{
			get => m_Alpha;
			set => m_Alpha = value;
		}

		[ XmlIgnore ]
		public System.Drawing.Color Color
		{
			get => System.Drawing.Color.FromArgb( m_Alpha, m_Red, m_Green, m_Blue );
			set
			{
				m_Red = value.R;
				m_Green = value.G;
				m_Blue = value.B;
				m_Alpha = value.A;
			}
		}

		public ColorDef()
		{
		}
		
		public ColorDef( System.Drawing.Color color )
		{
			Color = color;
		}
	}
}
