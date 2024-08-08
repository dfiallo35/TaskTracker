import { Component, inject } from '@angular/core';
import { Task } from '../../services/task';
import { TaskComponent } from "../task/task.component";
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [TaskComponent, CommonModule, FontAwesomeModule],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})
export class TaskListComponent {
  taskService: TaskService = inject(TaskService);
  taskList: Task[] = [];

  // ICONS
  faPlus = faPlus;

  constructor() {
    this.taskList = this.taskService.getAllTasks();
  }

  addTask(task: HTMLInputElement){
    if (task.value !== '') {
      this.taskService.addTask({name: task.value, isCompleted: false});
      this.taskList = this.taskService.getAllTasks();
      task.value = '';
    }
  }

  getCurrentDate(): string {
    return new Date().toDateString();
  }

  deleteTask(task: Task){
    this.taskService.deleteTask(task.id);
    this.taskList = this.taskService.getAllTasks();
  }
}
