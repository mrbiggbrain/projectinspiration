using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectInspirationLibrary.Flair.Comments
{
    public static class Comments
    {
        public static string DeathSaveComment(bool pass)
        {
            List<string> comments;
            var random = new Random();

            if(pass)
            {
                comments = File.ReadAllLines(@"flair\Comments\DeathSaves\Success.txt").ToList();
                
            }
            else
            {
                comments = File.ReadAllLines(@"flair\Comments\DeathSaves\Failure.txt").ToList();
            }

            return comments.ElementAt(random.Next(comments.Count));
        }
    }
}
