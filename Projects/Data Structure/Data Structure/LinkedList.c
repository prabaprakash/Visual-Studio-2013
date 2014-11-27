#include<stdio.h>
#include<stdlib.h>
/*
struct Node{
	int data;
	struct Node *next;
};
struct Node *head;
void insert(int x);
void print();
mainff()
{
	
	head = NULL;
	printf("how many numbers");
	int n, i, x;
	scanf("%d", &n);
	for (i = 0; i < n; i++)
	{
		printf("Enter the Number");
		scanf("%d", &x);
		insert(x);
		
	}
	print();
	getch();                               

}
void insert(int x)
{
	struct Node *temp= (struct Node*)malloc(sizeof(struct Node));
	printf("%p", temp);
	temp->data = x;
	temp->next = head;
	head = temp;
	
}
void print()
{
	struct Node *temp = head;
	while (temp!=NULL)
	{
		printf("%d",temp->data);
		temp = temp->next;
	}
}*/