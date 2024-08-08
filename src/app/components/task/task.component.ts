import { Component, inject, Output, EventEmitter } from '@angular/core';
import { Input } from '@angular/core';
import { Task } from '../../services/task';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { TaskService } from '../../services/task.service';


@Component({
  selector: 'app-task',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent {
  @Input() task!:Task;
  @Output() deletedTask = new EventEmitter<Task>();

  taskService: TaskService = inject(TaskService);
  faEdit = faEdit;
  faTrash = faTrash;
  
  editTask(task:Task){
    alert("Coming soon!");
  }
  
  deleteTask(task:Task){
    this.deletedTask.emit(task);
  }
}
