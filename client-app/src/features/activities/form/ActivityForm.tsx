
import { Button, Form, Segment } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import { ChangeEvent, useState } from "react";

interface Props{
    activity : Activity | undefined;
    closeForm: () => void;
    createOrEdit: (activity: Activity) => void;
}

export default function ActivityForm({activity: selectedActivity, closeForm, createOrEdit} : Props){

    const intialState  = selectedActivity ??{
        id: '',
        title: '',
        category: '',
        description: '',
        date: '',
        location: ''
    }

    const [activity, setActivity] = useState(intialState);

    function handleSubmit(){
        createOrEdit(activity);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>){
        const {name, value} = event.target;
        setActivity({...activity, [name] : value})
    }

    return(
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input placeholder='Title' value={activity.title} name='title' onChange={handleInputChange}/>
                <Form.Input placeholder='Description' value={activity.description} name='description' onChange={handleInputChange}/>
                <Form.Input placeholder='Category' value={activity.category} name='category' onChange={handleInputChange}/>
                <Form.Input placeholder='Date' value={activity.date} name='date' onChange={handleInputChange}/>
                <Form.Input placeholder='Location' value={activity.location} name='location' onChange={handleInputChange}/>
                <Button floated="right" positive type='submit' content='Submit' />
                <Button onClick={closeForm} floated="right" type='button' content='Cancel' />
            </Form>
        </Segment>
    )
}