export interface ContactSearch {

  keyword?: string;

  jobId?: number | null;

  departmentId?: number | null;

  dateOfBirthFrom?: string;

  dateOfBirthTo?: string;

  pageNumber: number;

  pageSize: number;

}