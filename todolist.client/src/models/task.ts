export interface Task {
    id: number;
    title: string;
    isCompleted: boolean;
};

export const DefaultTask: Task = {
    id: 0,
    title: "",
    isCompleted: false
};
