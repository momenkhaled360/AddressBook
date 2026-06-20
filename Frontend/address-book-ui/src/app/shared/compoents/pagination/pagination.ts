import {
  Component,
  computed,
  input,
  output
} from '@angular/core';

@Component({

  selector: 'app-pagination',

  standalone: true,

  imports: [],

  templateUrl: './pagination.html'

})

export class Pagination {

  totalCount = input.required<number>();

  pageNumber = input.required<number>();

  pageSize = input.required<number>();

  pageChanged = output<number>();

  totalPages = computed(() =>
    Math.max(1, Math.ceil(this.totalCount() / this.pageSize()))
  );

  pages = computed(() => {

    const total = this.totalPages();

    const current = this.pageNumber();

    const range: number[] = [];

    const start = Math.max(1, current - 1);

    const end = Math.min(total, current + 1);

    for (let i = start; i <= end; i++) {

      range.push(i);

    }

    return range;

  });

  goTo(page: number): void {

    if (page < 1 || page > this.totalPages() || page === this.pageNumber()) {

      return;

    }

    this.pageChanged.emit(page);

  }

}