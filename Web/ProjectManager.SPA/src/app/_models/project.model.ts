export interface IProjectModel {
    UserId: number;
    ProjectName: string;
    ProjectStartDate: Date;
    ProjectEndDate: Date;
    ProjectPriority: number;
    IsProjectSuspended: boolean;
    ManagerId: number;
    ManagerName: string;
}
