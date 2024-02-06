import { useField } from "formik";
import { Form, Label } from "semantic-ui-react";
import DatePicker, {ReactDatePickerProps} from 'react-datepicker';

interface Props {
    placeholder: string;
    name: string;
    label?: string;
}

export default function MyTextInput(props: Partial<ReactDatePickerProps>){
    const [field, meta, helpers] = useField(props.name!);
    return(
        <Form.Field error={meta.touched && !!meta.error}> {/*The double !! makes meta object a boolean */}
            <DatePicker 
                {...field}
                {...props}
                selected={(field.value && new Date(field.value)) || null}
                onChange={value => helpers.setValue(value)}
            />
            {meta.touched && meta.error ? (
                <Label basic color="red" >{meta.error}</Label>
            ): null}
        </Form.Field>
    )
}