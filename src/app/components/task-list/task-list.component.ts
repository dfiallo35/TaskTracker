import { Component, inject } from '@angular/core';
import { Task } from '../../services/task';
import { TaskComponent } from "../task/task.component";
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { ChangeDetectorRef } from '@angular/core';

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

  constructor(private cdr: ChangeDetectorRef) {
    this.getAllTasks();
  }

  async addTask(task: HTMLInputElement){
    if (task.value.trim() !== '') {
      await this.taskService.addTask({name: task.value, isCompleted: false});
      this.taskList = await this.taskService.getAllTasks();
      this.cdr.detectChanges();
      task.value = '';
    }
  }

  async getCurrentDate(): Promise<string> {
    return new Date().toDateString();
  }

  async deleteTask(task: Task){
    await this.taskService.deleteTask(task.id??0);
    await this.taskService.getAllTasks().then(data => this.taskList = data);
  }

  async getAllTasks(){
    this.taskList = await this.taskService.getAllTasks();
  }
}
