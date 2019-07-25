export interface IProjectModel {
    ProjectId: number;
    ProjectName: string;
    ProjectStartDate: Date;
    ProjectEndDate: Date;
    ProjectPriority: number;
    UserId: number;
    UserName: string;
    IsProjectSuspended: boolean;
}
