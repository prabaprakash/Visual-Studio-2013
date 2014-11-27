#include<stdio.h>
int maign()
{
	int *p;
	int c =10;
	p =&c;
	printf("%p", p);
	int urn[5] = { 100, 200, 300, 400, 500 };
	int * ptr1, *ptr2, *ptr3;
	ptr1 = urn; // assign an address to a pointer
	ptr2 = &urn[2]; // ditto
	// dereference a pointer and take
	// the address of a pointer
	int i = 0;
	printf("pointer value, dereferenced pointer, pointer address:\n");
	for ( i = 0;i < 5; i++)
	{
		ptr1 = &urn[i];
		printf("ptr1 = %p, *ptr1 =%d, &ptr1 = %p\n",
			ptr1, *ptr1, &ptr1);
	}
	


	
	getch();
	return 0;
}