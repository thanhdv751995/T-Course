import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { CategoryService, CategoryDto } from '@proxy/danh-mucs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class CategoryComponent implements OnInit {
  category = { items: [], totalCount: 0 } as PagedResultDto<CategoryDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedCategory = {} as CategoryDto;

  constructor(
    public readonly list: ListService,
    private CategoryService: CategoryService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService) {
  }

  ngOnInit(): void {
    const categoryStreamCreator = (query) => this.CategoryService.getList(query);

    this.list.hookToQuery(categoryStreamCreator).subscribe((response) => {
      this.category = response;
    });
  }
  createCategory() {
    this.selectedCategory = {} as CategoryDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editCategory(id: string) {
    this.CategoryService.get(id).subscribe((category) => {
      this.selectedCategory = category;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedCategory.name || '', Validators.required],
      description: [this.selectedCategory.description || '', Validators.required],
      idParent: [this.selectedCategory.idParent || null],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedCategory.id) {
      this.CategoryService
        .update(this.selectedCategory.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.CategoryService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.CategoryService.delete(id).subscribe(() => this.list.get());
        }
      });
  }

}
