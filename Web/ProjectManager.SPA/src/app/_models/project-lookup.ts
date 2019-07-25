import { IKeyValue } from './key-value';
import { IProjectModel } from './project.model';

export interface IProjectLookup {
    Users: IKeyValue[];
    Projects: IProjectModel[];
}
