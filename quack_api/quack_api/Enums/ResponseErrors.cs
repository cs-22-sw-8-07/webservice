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
         * 200-299 Recommender-controller-related errors
         */

        // System errors
        AnExceptionOccurredInAController = 1,
        AnExceptionOccurredInTheSAL = 2,
        ChangesCouldNotBeAppliedToTheDatabase = 5,
        QuackUpdateListBadFormat = 50,
        // Recommender-related errors
        NoConfigFile = 105,
        CouldNotInitializeRecommender = 109,
        CouldNotInitializeSpotipy = 110,
        CouldNotInitializeVectorSpace = 111,
        CouldNotFindClosestTracks = 112,
        CouldNotFindSongsFromPlaylist = 113,
        CouldNotFormatSongListToJson = 114,
        QuackLocationTypeArgumentNotANumber = 115,
        QuackLocationTypeNotWithinRange = 116,
        CouldNotFindTracksFromRangeRecommender = 117,
        CouldNotInitializeRangeModel = 118,
        CouldNotLoadDataSet = 119,
        CouldNotInitializeVectorSpaceModel = 120,
        Argument3NotGiven = 121,
        Argument3NotARecommender = 122,
        CouldNotLoadTrackData = 123,
        // Recommender-controller-related errors
        SomethingWentWrongInTheRecommender = 201,
        PathNull = 202,
        PathNotFound = 203,
        ProcessCouldNotStart = 204,
        RecommenderPathWrong = 205,
        ResultFromCommandlineEmpty = 206
    }
}
