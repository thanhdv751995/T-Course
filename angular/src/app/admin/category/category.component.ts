import { CategoryDto } from '@proxy/danh-mucs';
import { CategoryService } from './../../proxy/danh-mucs/category.service';
import { CategoryLookupDto,CourseService } from '@proxy/khoa-hocs';
import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter,  }],
})
export class CategoryComponent implements OnInit {
  public SkipCount = 1;
  public MaxResultCount = 10;
  searchText;
  categories = { items: [], totalCount: 0 } as PagedResultDto<CategoryDto>;
 
  CategoryList:any;

  isModalOpen = false;

  form: FormGroup;

  selectedCategory = {} as CategoryDto;
  categories$: Observable<CategoryLookupDto[]>;
  constructor(
    private courseService: CourseService,
    public readonly list: ListService,
    private categoryService: CategoryService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    ) {
  }

  ngOnInit(): void {
    const categoryStreamCreator = (query) => this.categoryService.getList(query);

    this.list.hookToQuery(categoryStreamCreator).subscribe((response) => {
      this.categories = response;
    });
    this.categories$ = this.categoryService.getCategoryLookup().pipe(map((r) => r.items));
  }

  createCategory() {
    this.selectedCategory = {} as CategoryDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editCategory(id: string) {
    this.categoryService.getID(id).subscribe((category) => {
      this.selectedCategory = category;
      this.buildForm();
      this.isModalOpen = true;
      console.log(id)
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
      this.categoryService
        .update(this.selectedCategory.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.categoryService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete (id: string)
  {
    this.courseService.getListCourseByCategoryIDByID(id).subscribe(data=>{
      console.log(data)
      if(data.items.length > 0 ){
        this.confirmation.warn('::Bạn sẽ xóa hết khóa học thuộc Danh mục này', '::Chắc chắn xóa')
      .subscribe((status) => {
        if (status === Confirmation.Status.confirm) {
          this.categoryService.delete(id).subscribe(() => 
          this.list.get());
        }
      });
      }else{
        this.confirmation.warn('', '::Chắc chắn xóa')
        .subscribe((status) => {
          if (status === Confirmation.Status.confirm) {
              this.categoryService.delete(id).subscribe(() => 
              this.list.get());
          }
        });
      }

    });  
  }
  key = 'id';
  reserve: boolean = false;
  sort(key) {
    this.key = key;
    this.reserve = !this.reserve;
  }

}

