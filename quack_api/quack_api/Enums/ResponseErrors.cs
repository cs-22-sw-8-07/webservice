using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quack_api.Enums
{
    public enum ResponseErrors
    {
        /* Response codes for categorization:
         * 0-99 - System Errors
         * 100-199 Recommender-related errors             
         */

        // System errors
        AnExceptionOccurredInAController = 1,
        AnExceptionOccurredInTheSAL = 2,
        ChangesCouldNotBeAppliedToTheDatabase = 5,
        QuackUpdateListBadFormat = 50,
        // Recommender-related errors
        PathToPythonExeNotFound = 100,
        SomethingWentWrongInTheRecommender = 101,
        CouldNotInitializeSpotipy = 110,
        CouldNotFindPlaylists = 111,
        CouldNotFindSongsFromPlaylist = 112,
        CouldNotFormatSongListToJson = 113

    }
}
