import { User } from "./user";

export interface Course {
  id: string;
  name: string;
  introduction: string;
  imageCourse: string;
  code: string;
  price: number;
  totalCourseDuration: number;
  numberOfStudent: number;
  numberOfPurchases: number;
  creator: User;
  updateTime: Date;
  requirement: string;
  subjects: any;

  // creator: User;
  // public virtual ICollection<Subject>? Subjects { get; set; }
  // public virtual ICollection<RegisterStudy>? RegisterStudies { get; set; }
  // public virtual ICollection<Bill>? Bills { get; set; }
}
