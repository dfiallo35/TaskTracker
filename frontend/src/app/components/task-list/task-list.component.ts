import { Component, inject } from '@angular/core';
import { Task } from '../../services/task';
import { TaskComponent } from "../task/task.component";
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [TaskComponent, CommonModule],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})
export class TaskListComponent {
  taskService: TaskService = inject(TaskService);
  taskList: Task[] = [];

  constructor() {
    this.taskList = this.taskService.getAllTasks();
  }

  addTask(task: HTMLInputElement){
    this.taskService.addTask({name: task.value, isCompleted: false});
    this.taskList = this.taskService.getAllTasks();
  }
}
