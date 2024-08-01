import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { Task } from '../../services/task';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { faTrash } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-task',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent {
  @Input() task!:Task;
  faEdit = faEdit;
  faTrash = faTrash;

}
