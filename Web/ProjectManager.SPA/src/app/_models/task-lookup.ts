import { IKeyValue } from './key-value';

export interface ITaskLookup {
    Projects: IKeyValue[];
    ParentTasks: IKeyValue[];
    Users: IKeyValue[];
}
