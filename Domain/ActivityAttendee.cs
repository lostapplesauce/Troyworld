using System;

namespace Domain
{
    public class ActivityAttendee
    {
        public string AppUserId {get; set;}
        public AppUser AppUser {get; set;}
        //Misspelling here I am aware of but shall just leave it
        // because it's in the database right now this way
        public Guid AcitivityId {get; set;}
        public Activity Activity{get; set;}
        public bool IsHost{get; set;}
    }
}