//-----------------------------------------------------------------------
// <copyright file="Comments.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace ProjectInspirationLibrary.Flair
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Handles loading random comments for message flair.
    /// </summary>
    public static class Comments
    {
        /// <summary>
        /// Returns a random death save comment. 
        /// </summary>
        /// <param name="pass">Did the save pass or fail?</param>
        /// <returns>The death save comment.</returns>
        public static string DeathSaveComment(bool pass)
        {
            List<string> comments;
            var random = new Random();

            comments = pass
                ? File.ReadAllLines(@"flair\Comments\DeathSaves\Success.txt").ToList()
                : File.ReadAllLines(@"flair\Comments\DeathSaves\Failure.txt").ToList();

            return comments.ElementAt(random.Next(comments.Count));
        }
    }
}
