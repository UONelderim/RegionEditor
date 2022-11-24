using System;
using System.Drawing;

namespace TheBox.MapViewer
{
	/// <summary>
	/// Provides the sizes of the UO maps
	/// </summary>
	public class MapSizes
	{
		// Issue 13 - ML map sizes - http://code.google.com/p/pandorasbox3/issues/detail?id=13 - Kons
		/// <summary>
		/// Gets the size of the Felucca ML map
		/// </summary>
		public static Size FeluccaML => new Size(7168, 4096);
		// Issue 13 - End

		/// <summary>
		/// Gets the size of the Felucca map
		/// </summary>
		public static Size Felucca => new Size(6144, 4096);

		/// <summary>
		/// Gets the size of the Trammel map
		/// </summary>
		public static Size Trammel => new Size( 6144, 4096 );

		/// <summary>
		/// Gets the size of the Ilshenar map
		/// </summary>
		public static Size Ilshenar => new Size( 2304, 1600 );

		/// <summary>
		/// Gets the size of the Malas map
		/// </summary>
		public static Size Malas => new Size( 2560, 2048 );

		/// <summary>
		/// Gets the size of the Tokuno islands map
		/// </summary>
		public static Size Tokuno => new Size( 1448, 1448 );

		/// <summary>
		/// Gets the size of the Tokuno islands map
		/// </summary>
		public static Size Termur => new Size(1280, 4096);

		/// <summary>
        /// Gets the size of a map
        /// </summary>
        /// <param name="mapfile">The index of the map</param>
        /// <returns>A Size object representing the size of the map</returns>
        public static Size GetSize( int mapfile )
		{
			switch ( mapfile )
			{
				case 0:
				case 1:

					return FeluccaML;

				case 2:

					return Ilshenar;

				case 3:

					return Malas;

				case 4:

					return Tokuno;

                case 5:

                    return Termur;
            }

			throw new Exception($"Map file {mapfile} not supported");
		}
	}
}
