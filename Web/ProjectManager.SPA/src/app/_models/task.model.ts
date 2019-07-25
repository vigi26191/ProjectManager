export interface ITaskModel {
    TaskId: number;
    TaskName: string;
    TaskPriority: number;
    IsParentTask: boolean;
    TaskStartDate: Date;
    TaskEndDate: Date;
    IsTaskComplete: boolean;
    ProjectId: number;
    UserId: number;
    ParentTaskId: number;
    ParentTaskName: string;
}
