import { Profile } from "./profile";

// This is set 
export interface Activity {
    id: string;
    title: string;
    date: Date | null;
    description: string;
    category: string;
    location: string;
    hostUsername?: string;
    isCancelled?: boolean;
    isGoing: boolean;
    isHost: boolean;
    host?: Profile;
    attendees: Profile[]
  }

  export class Activity implements Activity{
    constructor(init?: ActivityFormValues){
      Object.assign(this, init);
    }
  }

  export class ActivityFormValues{
    id?: string = undefined;
    title: string = '';
    category: string = '';
    description: string = '';
    date: Date | null = null;
    location: string = '';

    constructor(activity?: ActivityFormValues){
      if(activity){
        this.id = activity.id;
        this.title = activity.title;
        this.category = activity.category;
        this.description = activity.description;
        this.date = activity.date;
        this.location = activity.location;
      }
    }
  }