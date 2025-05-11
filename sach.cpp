#include<stdio.h>
#include<string.h>
typedef struct sach{
	char tensach[30];
	char tentg[30];
	int sotrang;
};

void nhap(sach a[], int n){
	int i;
	for(i = 0; i < n; i++){
		printf("Cuon sach thu %d\n", i+1);
		printf("Ten sach: ");
		fflush(stdin);
		gets(a[i].tensach);
		printf("Ten tac gia: ");
		fflush(stdin);
		gets(a[i].tentg);
		sach:
		printf("So trang: ");
		scanf("%d", &a[i].sotrang);
		if(a[i].sotrang <= 0){
			printf("Moi ban nhap lai\n");
			goto sach;
		}
	}
}
void xem(sach a[], int n){
	int i;
	printf("Ho ten\tTentg\tSo trang\n");
	for(i = 0; i < n; i++){
		printf("%s\t%s\t%d\n", a[i].tensach, a[i].tentg, a[i].sotrang);
	}
}

int demsotrang(sach a[], int n){
	int i,dem = 0;
	for(i = 0; i < n; i++){
		if(a[i].sotrang >= 200)
			++dem;
	}
	return dem;
}
int main(){
	sach a[50];
	int n;
	nhap:
	printf("Nhap so cuon sach: ");
	scanf("%d", &n);
	if(n < 0){
		printf("Moi ban nhap lai\n");
		goto nhap;
	}
	nhap(a, n);
	xem(a, n);
	printf("\n\nSo cuon sach lon hon 200 trang la : %d", demsotrang(a, n));
}
