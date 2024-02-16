import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/serverError";

export default class CommonStore{
    error: ServerError | null = null;
    token: string | null | undefined = localStorage.getItem('jwt'); //
    appLoaded = false;

    constructor(){
        makeAutoObservable(this);

        //This reaction only runs when token has a change 
        //autoreaction changes initally when class starts
        reaction(
            () => this.token,
            token =>  {
                if(token){
                    localStorage.setItem('jwt', token)
                } else {
                    localStorage.removeItem('jwt')
                }
            })
    }
    
    setServerError(error: ServerError){
        this.error = error;
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    setAppLoaded = () =>{
        this.appLoaded = true;
    }
}