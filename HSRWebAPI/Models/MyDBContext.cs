using System.Collections.ObjectModel;

namespace HSRWebAPI.Models
{
    public class MyDBContext
    {
        public static List<Blessing> Blessings { get; set; } = new List<Blessing>()
        {
            new Blessing()
            {
                Id = "BL-01",
                Name = "Sensory Labyrinth",
                ImageUrl = "abcxyz",
                Description = "Extends the duration of Wind Shear, Bleed, Shock, and Burn on enemies by 1 turn(s).",
                Path = BlessingPath.Nihility,
                Level = 1
            },
            new Blessing()
            {
                Id = "BL-02",
                Name = "Night Beyond Pyre",
                ImageUrl = "abcxyz",
                Description = "Increases Weakness Break efficiency by 30%.",
                Path = BlessingPath.Nihility,
                Level = 2
            },
            new Blessing()
            {
                Id = "BL-03",
                Name = "Perfect Experience: Fuli",
                ImageUrl = "abcxyz",
                Description = "When attacking Frozen enemies, there is a 100% base chance to inflict Dissociation for 1 turn(s).",
                Path = BlessingPath.Remembrance,
                Level = 3
            },
            new Blessing()
            {
                Id = "BL-04",
                Name = "Spore Discharge",
                ImageUrl = "abcxyz",
                Description = "For each Skill Point a character consumes, all enemies will gain 1 Spore(s).",
                Path = BlessingPath.Propagation,
                Level = 3
            },
            new Blessing()
            {
                Id = "BL-05",
                Name = "Empyrean Imperium",
                ImageUrl = "abcxyz",
                Description = "When a character's turn begins, they gain 1 stack(s) of Critical Boost.",
                Path = BlessingPath.The_Hunt,
                Level = 3
            },

        };

        public static List<Blessing> MyBlessings { get; set; } = new List<Blessing>();

        // Add a Blessing to MyBlessing list based on ID.
        // @param id: the ID of the Blessing to add
        // @returns a code indicating operation status
        public static int AddBlessingById(string id)
        {
            // 1. Find the blessing in the list that matches the ID 
            var result = Blessings.FirstOrDefault<Blessing>(item => item.Id.Equals(id));
            bool isExistBlessing = MyBlessings.Any(blessing => blessing.Id.Equals(id));
            int statusCode = 404;   // Not Found by default
            // Blessing exists
            if (result != null)
            {
                if (isExistBlessing)    // Blessing already obtained => Bad Request
                {
                    statusCode = 400;
                }
                else
                {   // Add Blessing to list
                    MyBlessings.Add(result);
                    statusCode = 201;   // => Created
                }
            }
            return statusCode;
        }

        // Get a MyBlessing list consisting of all obtained Blessings.
        // @returns a code indicating operation status
        public static int GetAllMyBlessings()
        {
            int statusCode = 404;   // Not Found by default

            // 1. Get list of my Blessings...
            var result = MyBlessings.ToList();
            // ...and check if there is any element(s)
            bool isExistList = (result.Count != 0);

            // 2. Process result
            if (isExistList)     // the retrieved list is not empty
            {
                statusCode = 200; // => OK
            }
            return statusCode;
        }

        // Delete a Blessing from MyBlessing list based on ID.
        // @param id: The ID of the Blessing to delete
        // @returns a code indicating operation status
        public static int DeleteBlessingById(string id)
        {
            int statusCode = 404;   // Not Found by default
            bool isEmptyList = (MyBlessings.Count == 0);

            if (isEmptyList)        // MyBlessing list is empty...
            {                       // ...but still call for deletion
                statusCode = 400;   // => Bad Request
            }
            else
            {
                // 1. Find a match in MyBlessing list
                var result = MyBlessings.FirstOrDefault<Blessing>(item => item.Id.Equals(id));
                if (result != null)     // A result match exists                                        
                {                       // => Proceed to deletion
                    MyBlessings.Remove(result);
                    statusCode = 200;   // => OK (indicates a successful deletion)
                }
            }
            return statusCode;
        }

        // Show Blessings that haven't been obtained
        // @returns a code indicating operation status
        public static List<Blessing> ShowUnobtainedBlessings()
        {
            List<Blessing> missingBlessings = new();
            int statusCode = 404;   // Not Found by default
            // Get the Blessing list having My Blessings ruled out
            var result = Blessings.Except(MyBlessings);
            if (result.Any())    // The result is not empty
            {
                statusCode = 200;   // OK (a successful request)
                foreach (var item in result)
                {
                    missingBlessings.Add(item);
                }
            }
            return missingBlessings;
        }


        // Group Blessings by Category and their Count
        // @param blessings: List of Obtained Blessings
        // @returns a List consisting of Blessing Paths and their Blessing count
        public static List<BlessingPathCount> GetBlessingPathCounts(List<Blessing> blessings)
        {
            var result = blessings                              // => Blessing List
                        .GroupBy(blessing => blessing.Path)     // => Group by Blessing Path
                        .Select(group => new BlessingPathCount  // => Select each group to a BlessingPathCount object
                        {
                            Path = group.Key,                   // => Set properties
                            Count = group.Count()
                        })
                        .ToList();                              // => Add groups as a List

            return result;
        }
        // List and Count all Blessings by Path
        // @returns a List containing all obtained Blessings with count

        // INCOMPLETE
        public static List<BlessingPathCount> GetBlessingsCountByPath()
        {
            var myBlessings = MyBlessings;
            List<BlessingPathCount> result = new();

            if (myBlessings != null)    // MyBlessing list is not empty
            {
                // Get groups of Blessing Paths and their count
                var blessingPathCounts = GetBlessingPathCounts(myBlessings);
                // If Count of a Path = 0, then its Blessing List should be empty
                foreach (var item in blessingPathCounts)    // Iterate over each group
                {
                    Console.WriteLine(item.ToString());
                }
            }
            return result;
        }
    }
}
