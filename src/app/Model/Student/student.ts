export interface Student {
    studentID : bigint | string;
    studentRegNo:string;
    firstName:string;
    middleName:string | null;
    lastName:string;
    displayName:string;
    email:string;
    gender:string;
    dob:Date;
    address:string;
    contactNo:string;
    isEnable:boolean;
}
