namespace TowerOfDefence.Game
{
    [System.Serializable]
    public struct TowerUpgradeInfo
    {
        public float towerRange;
        public float bulletFireTime;
        public int bulletDamage;
        public int bulletSpeed;
        public int price;
        public TowerUpgradeInfo(float towerRange, float fireTime,int bulletDamage, int bulletSpeed,int price)
        {
            this.towerRange = towerRange;
            this.bulletFireTime = fireTime;
            this.bulletDamage = bulletDamage;
            this.bulletSpeed = bulletSpeed;
            this.price = price;
        }
    }
}
