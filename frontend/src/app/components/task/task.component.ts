import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { Task } from '../../services/task';

@Component({
  selector: 'app-task',
  standalone: true,
  imports: [],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent {
  @Input() task!:Task;
}
