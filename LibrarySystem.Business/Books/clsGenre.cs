using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Pure domain entity representing a literary genre / book category.
    /// Provides retrieval and lookup methods for catalog classification.
    /// </summary>
    public class clsGenre
    {
        public int GenreID { get; private set; } = -1;
        public string GenreName { get; set; } = string.Empty;

        public clsGenre()
        {
            this.GenreID = -1;
            this.GenreName = string.Empty;
        }

        private clsGenre(int genreID, string genreName)
        {
            this.GenreID = genreID;
            this.GenreName = genreName;
        }

        /// <summary>
        /// Finds a genre by its primary key.
        /// </summary>
        public static clsGenre Find(int genreID)
        {
            string genreName = string.Empty;

            if (clsGenreDataAccess.GetGenreInfoByID(genreID, ref genreName))
            {
                return new clsGenre(genreID, genreName);
            }

            return null;
        }

        /// <summary>
        /// Adds a new genre if it does not already exist.
        /// </summary>
        public static int AddNew(string genreName)
        {
            if (string.IsNullOrWhiteSpace(genreName))
                throw new ArgumentException("Genre name cannot be empty.", nameof(genreName));

            return clsGenreDataAccess.AddNewGenre(genreName.Trim());
        }

        /// <summary>
        /// Retrieves all genres for binding to dropdown selectors.
        /// </summary>
        public static DataTable GetAllGenres()
        {
            return clsGenreDataAccess.GetAllGenres();
        }
    }
}
