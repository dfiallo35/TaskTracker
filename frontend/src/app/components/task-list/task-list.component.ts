import { Component } from '@angular/core';
import { Task } from '../../services/task';
import { TaskComponent } from "../task/task.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [TaskComponent, CommonModule],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})
export class TaskListComponent {
  taskList: Task[] = [{id:1,name:"Pan"}];

  public appendTask(elem: HTMLInputElement) {
    const newTask:Task = {name: elem.value}
    elem.value="";
    this.taskList.push(newTask);    
  }
}