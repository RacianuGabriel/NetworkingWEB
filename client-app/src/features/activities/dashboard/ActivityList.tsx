import React from "react";
import { Activity } from "../../../app/models/activity";
import { Button, Item, Label, Segment } from "semantic-ui-react";

interface Props {
	  activities: Activity[];
	  selectActivity: (id: string) => void;
	  deleteActivity: (id: string) => void;
	  submitting: boolean;
}

export default function ActivityList({activities,selectActivity, deleteActivity, submitting}: Props) {
  const [target, setTarget] = React.useState('' as any);

  function handleActivityDelete(e: React.MouseEvent<HTMLButtonElement, MouseEvent>, id: string) {
	setTarget(e.currentTarget.name);
	deleteActivity(id);
  }
	
	return (
	<Segment>
		<Item.Group divided>
			{activities.map(activity => (
				<Item key={activity.id}>
					<Item.Content>
						<Item.Header as='a'>{activity.title}</Item.Header>
						<Item.Meta>{activity.date}</Item.Meta>
						<Item.Description>
							<div>{activity.description}</div>
							<div>{activity.city}, {activity.venue}</div>
						</Item.Description>
						<Item.Extra>
							<Button onClick={() => selectActivity(activity.id)} floated='right' content='View' color='teal'/>
							<Button 
								name={activity.id}
								loading={submitting && target === activity.id}
								onClick={(e) => handleActivityDelete(e,activity.id)}
								floated='right' 
								content='Delete' 
								color='red'
							/>
							<Label basic content={activity.category}/>
						</Item.Extra>
					</Item.Content>
				</Item>
			))}
		</Item.Group>
	</Segment>
  );
}