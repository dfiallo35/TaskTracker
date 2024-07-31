import { Injectable } from '@angular/core';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  protected tasks: Task[] = [
    { id: 1, name: 'Task 1', isCompleted: false },
    { id: 2, name: 'Task 2', isCompleted: false },
    { id: 3, name: 'Task 3', isCompleted: false },
    { id: 4, name: 'Task 4', isCompleted: false }
  ]

  constructor() { }

  getAllTasks(): Task[]{
    return this.tasks;
  }

  addTask(task: Task): void{
    this.tasks.push(task);
  }

  deleteTask(taskId: number): void{
    this.tasks = this.tasks.filter(task => task.id !== taskId)
  }
}
