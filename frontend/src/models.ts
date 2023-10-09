export class Box{
  id?: number;
  volume?: number;
  name?: string;
  color?: string;
  description?: string;
}

export class ResponseDto<T>{
  responseData?: T;
  messageToClient?: string;
}
