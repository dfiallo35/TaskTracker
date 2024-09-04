import { Injectable } from '@angular/core';
import { Task } from './task';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  API_URL = environment.API_URL;
  API_KEY = environment.API_KEY;
  
  constructor() { }

  async getAllTasks(): Promise<Task[]>{
    try {
      const data = await fetch(this.API_URL,
        {
          headers: {
            'API-Key': this.API_KEY,
            'accept': 'application/json'
          }
        }
      );
      return data.json();
    }
    catch (error) {
      console.error(error);
      return [];
    }
  }

  async addTask(task: Task): Promise<void>{
    try {
      await fetch(this.API_URL, {
        method: 'POST',
        body: JSON.stringify(task),
        headers: {
          'API-Key': this.API_KEY,
          'accept': 'application/json',
          'Content-Type': 'application/json',
        }
      });
    }
    catch (error) {
      console.error(error);
    }
  }

  async getTaskById(taskId: number): Promise<Task|null>{
    try {
      const data = await fetch(`${this.API_URL}/${taskId}`, {
        headers: {
          'API-Key': this.API_KEY,
          'accept': 'application/json'
        }
      });
      return data.json();
    }
    catch (error) {
      console.error(error);
      return null;
    }
  }

  async deleteTask(taskId: number): Promise<void>{
    try {
      await fetch(`${this.API_URL}/${taskId}`, {
        method: 'DELETE',
        headers: {
          'API-Key': this.API_KEY,
          'accept': '*/*'
        }
      });
    }
    catch (error) {
      console.error(error);
    }
  }
}
