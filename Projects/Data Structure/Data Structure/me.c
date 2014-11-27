#include<stdio.h>
#include<conio.h>

struct Node
{
	int data;
	struct Node *link;
};
struct Node *head;
void add(int x);
void print();
void main()
{
	head = NULL;
	int x, n,i=0;
	scanf("%d", &n);
	for (i = 0; i < n; i++)
	{
		scanf("%d", &x);
		add(x);
	}
	print();
}
void add(int x)
{
	struct  Node *temp = (struct Node*)malloc(sizeof(struct Node));
	temp->data = x;
	temp->link = head;
	head = temp;
}
void print()
{
	struct Node *a = head;
	while (a != NULL)
	{
		printf("%d", a->data);
		a = a->link;
	}
}
